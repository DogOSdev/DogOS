FROM gitpod/workspace-full

# Gitpod uses Ubuntu 20.04, so we install the .NET version for it.

# Install the signing key
RUN wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb -O /home/gitpod/packages-microsoft-prod.deb
RUN sudo dpkg -i /home/gitpod/packages-microsoft-prod.deb
RUN rm /home/gitpod/packages-microsoft-prod.deb

# Install .NET SDK 5.0
RUN sudo apt-get update
RUN sudo apt-get install -y apt-transport-https
RUN sudo apt-get update
RUN sudo apt-get install -y dotnet-sdk-5.0

# Install NuGet
RUN sudo apt-get install nuget -y

# "Install" Cosmos
RUN sudo mkdir -p /home/gitpod/.nuget/packages/
RUN sudo wget -O /home/gitpod/cosmos_source.zip https://github.com/CosmosOS/Cosmos/archive/refs/tags/Userkit_20200708.zip
RUN sudo unzip /home/gitpod/cosmos_source.zip -d /home/gitpod/
RUN sudo rm -rf \
    /home/gitpod/Cosmos-Userkit_20200708/source/Archive/ \
    /home/gitpod/Cosmos-Userkit_20200708/source/Cosmos.VS.ReadMe.html \
    /home/gitpod/Cosmos-Userkit_20200708/source/Cosmos.VS.Windows \
    /home/gitpod/Cosmos-Userkit_20200708/source/Kernel-TapRoot \
    /home/gitpod/Cosmos-Userkit_20200708/source/Kernel-X86 \
    /home/gitpod/Cosmos-Userkit_20200708/source/Templates \
    /home/gitpod/Cosmos-Userkit_20200708/source/TheRingMaster \
    /home/gitpod/Cosmos-Userkit_20200708/source/Tools
RUN sudo mv /home/gitpod/Cosmos-Userkit_20200708/source/* /home/gitpod/.nuget/packages/
RUN sudo rm -rf /home/gitpod/Cosmos-Userkit_20200708 /home/gitpod/cosmos_source.zip

RUN sudo chmod ugo+rwx /home/gitpod/.nuget
RUN sudo chmod ugo+rwx /home/gitpod/.nuget/packages