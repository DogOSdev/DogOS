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
RUN sudo apt-get install nuget