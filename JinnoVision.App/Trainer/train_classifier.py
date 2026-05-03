import json
from pathlib import Path

import torch
import torch.nn as nn
import torch.optim as optim
from torchvision import transforms, models
from torch.utils.data import Dataset, DataLoader, random_split
from PIL import Image


ROOT = Path(r"C:\Users\user\source\repos\JinnoVision\JinnoVision.App")

TRAINING_DATA = ROOT / "TrainingData"
MODEL_DIR = ROOT / "Models"

MODEL_DIR.mkdir(exist_ok=True)

MODEL_FILE = MODEL_DIR / "component_passfail.onnx"
LABELS_FILE = MODEL_DIR / "labels.json"

IMG_SIZE = 224
BATCH_SIZE = 8
EPOCHS = 10
LEARNING_RATE = 0.0005

class ComponentPassFailDataset(Dataset):
    def __init__(self, root_dir, transform=None):
        self.root_dir = Path(root_dir)
        self.transform = transform
        self.samples = []
        self.classes = []

        valid_exts = [".png", ".jpg", ".jpeg", ".bmp", ".tif", ".tiff"]

        for component_dir in sorted(self.root_dir.iterdir()):
            if not component_dir.is_dir():
                continue

            component_name = component_dir.name

            for status in ["Pass", "Fail"]:
                status_dir = component_dir / status

                if not status_dir.exists():
                    continue

                class_name = f"{component_name}_{status}"

                if class_name not in self.classes:
                    self.classes.append(class_name)

                class_index = self.classes.index(class_name)

                for img_path in status_dir.iterdir():
                    if img_path.suffix.lower() in valid_exts:
                        self.samples.append((img_path, class_index))

        if len(self.samples) == 0:
            raise Exception("No training images found.")

    def __len__(self):
        return len(self.samples)

    def __getitem__(self, index):
        img_path, label = self.samples[index]

        image = Image.open(img_path).convert("RGB")

        if self.transform:
            image = self.transform(image)

        return image, label

def main():
    print("=== JinnoVision Real Trainer ===")
    print("Training data:", TRAINING_DATA)

    if not TRAINING_DATA.exists():
        raise Exception(f"TrainingData folder not found: {TRAINING_DATA}")

    transform = transforms.Compose([
        transforms.Resize((IMG_SIZE, IMG_SIZE)),
        transforms.RandomHorizontalFlip(),
        transforms.RandomRotation(8),
        transforms.ColorJitter(brightness=0.15, contrast=0.15),
        transforms.ToTensor(),
        transforms.Normalize(
            mean=[0.485, 0.456, 0.406],
            std=[0.229, 0.224, 0.225]
        )
    ])

    dataset = ComponentPassFailDataset(TRAINING_DATA, transform=transform)

    if len(dataset.classes) < 2:
        raise Exception("Need at least 2 classes. Example: Resistor/Pass and Resistor/Fail")

    if len(dataset) < 4:
        raise Exception("Need more images before training.")

    print("Classes found:")
    for index, name in enumerate(dataset.classes):
        print(index, name)

    with open(LABELS_FILE, "w") as f:
        json.dump(dataset.classes, f, indent=4)

    val_size = max(1, int(len(dataset) * 0.2))
    train_size = len(dataset) - val_size

    train_dataset, val_dataset = random_split(dataset, [train_size, val_size])

    train_loader = DataLoader(train_dataset, batch_size=BATCH_SIZE, shuffle=True)
    val_loader = DataLoader(val_dataset, batch_size=BATCH_SIZE, shuffle=False)

    device = torch.device("cuda" if torch.cuda.is_available() else "cpu")
    print("Device:", device)

    model = models.resnet18(weights=models.ResNet18_Weights.DEFAULT)

    for param in model.parameters():
        param.requires_grad = False

    model.fc = nn.Linear(model.fc.in_features, len(dataset.classes))
    model = model.to(device)

    criterion = nn.CrossEntropyLoss()
    optimizer = optim.Adam(model.fc.parameters(), lr=LEARNING_RATE)

    for epoch in range(EPOCHS):
        model.train()
        total_loss = 0

        for images, labels in train_loader:
            images = images.to(device)
            labels = labels.to(device)

            optimizer.zero_grad()

            outputs = model(images)
            loss = criterion(outputs, labels)

            loss.backward()
            optimizer.step()

            total_loss += loss.item()

        accuracy = evaluate(model, val_loader, device)

        print(
            f"Epoch {epoch + 1}/{EPOCHS} "
            f"Loss: {total_loss:.4f} "
            f"Val Accuracy: {accuracy:.2f}%"
        )

    export_onnx(model, device)

    print("Labels saved:", LABELS_FILE)
    print("ONNX model saved:", MODEL_FILE)
    print("Training completed successfully.")


def evaluate(model, loader, device):
    model.eval()

    correct = 0
    total = 0

    with torch.no_grad():
        for images, labels in loader:
            images = images.to(device)
            labels = labels.to(device)

            outputs = model(images)
            predictions = torch.argmax(outputs, dim=1)

            correct += (predictions == labels).sum().item()
            total += labels.size(0)

    if total == 0:
        return 0

    return correct / total * 100


def export_onnx(model, device):
    model.eval()

    dummy_input = torch.randn(1, 3, IMG_SIZE, IMG_SIZE).to(device)

    torch.onnx.export(
        model,
        dummy_input,
        str(MODEL_FILE),
        input_names=["input"],
        output_names=["output"],
        dynamic_axes={
            "input": {0: "batch_size"},
            "output": {0: "batch_size"}
        },
        opset_version=11
    )


if __name__ == "__main__":
    main()