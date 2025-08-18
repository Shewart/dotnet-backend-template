#!/bin/bash

# Bash script to install the .NET Backend Template locally

echo "🚀 Installing .NET Backend Template..."

# Check if dotnet CLI is available
if ! command -v dotnet &> /dev/null; then
    echo "❌ .NET CLI not found. Please install .NET SDK first."
    echo "   Download from: https://dotnet.microsoft.com/download"
    exit 1
fi

# Get current directory (template source)
TEMPLATE_PATH=$(pwd)

echo "📁 Template source: $TEMPLATE_PATH"

# Check if template config exists
if [ ! -f ".template.config/template.json" ]; then
    echo "❌ Template configuration not found. Make sure you're in the template root directory."
    exit 1
fi

echo "🔧 Installing template from current directory..."

# Install template from current directory
if dotnet new install "$TEMPLATE_PATH" --force; then
    echo ""
    echo "✅ Template installed successfully!"
    echo ""
    echo "🎯 Usage:"
    echo "  dotnet new dotnet-backend -n MyAwesomeAPI"
    echo ""
    echo "📋 Available parameters:"
    echo "  --Framework          Target framework (net9.0, net8.0)"
    echo "  --DatabaseProvider   Database provider (MongoDB, PostgreSQL, SqlServer)"
    echo "  --UseSwagger         Include Swagger documentation (true/false)"
    echo "  --IncludeTests       Include test projects (true/false)"
    echo "  --DatabaseName       Default database name"
    echo ""
    echo "📖 Example:"
    echo "  dotnet new dotnet-backend -n MyAPI --DatabaseProvider MongoDB --UseSwagger true"
    echo ""
    echo "🧪 Test it:"
    echo "  mkdir test-api && cd test-api"
    echo "  dotnet new dotnet-backend -n TestAPI"
    echo "  cd TestAPI && dotnet run --project TestAPI.Api"
else
    echo "❌ Failed to install template. Try running with --force flag:"
    echo "   dotnet new install \"$TEMPLATE_PATH\" --force"
    exit 1
fi
