#!/bin/bash
set -e

echo "Installing Google Chrome..."

sudo apt update
sudo apt install -y wget

wget -qO- https://dl.google.com/linux/linux_signing_key.pub | sudo gpg --dearmor -o /usr/share/keyrings/google-chrome-keyring.gpg
echo 'deb [signed-by=/usr/share/keyrings/google-chrome-keyring.gpg] http://dl.google.com/linux/chrome/deb/ stable main' | sudo tee /etc/apt/sources.list.d/google-chrome.list

sudo apt update
sudo apt install -y google-chrome-stable

echo "✅ Google Chrome installed successfully"

echo "Restoring solution..."

cd Solution/
dotnet restore
cd ..

echo "✅ Solution restored successfully"
