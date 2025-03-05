dotnet restore

echo "Installing Google Chrome..."
sudo apt update
sudo apt-get upgrade -y

sudo apt-get install -y libxss1 libappindicator3-1 libindicator7 fonts-liberation libdbus-1-3

wget https://dl.google.com/linux/direct/google-chrome-stable_current_amd64.deb
sudo dpkg -i google-chrome-stable_current_amd64.deb

google-chrome-stable --version && echo "✅ Google Chrome installed successfully"