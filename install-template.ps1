# PowerShell script to install the .NET Backend Template locally

Write-Host "🚀 Installing .NET Backend Template..." -ForegroundColor Green

# Check if dotnet CLI is available
if (!(Get-Command "dotnet" -ErrorAction SilentlyContinue)) {
    Write-Host "❌ .NET CLI not found. Please install .NET SDK first." -ForegroundColor Red
    exit 1
}

# Get current directory (template source)
$templatePath = $PSScriptRoot

Write-Host "📁 Template source: $templatePath" -ForegroundColor Yellow

# Install template from current directory
try {
    dotnet new install $templatePath
    Write-Host "✅ Template installed successfully!" -ForegroundColor Green
    Write-Host ""
    Write-Host "🎯 Usage:" -ForegroundColor Cyan
    Write-Host "  dotnet new dotnet-backend -n MyAwesomeAPI" -ForegroundColor White
    Write-Host ""
    Write-Host "📋 Available parameters:" -ForegroundColor Cyan
    Write-Host "  --Framework          Target framework (net9.0, net8.0)" -ForegroundColor White
    Write-Host "  --DatabaseProvider   Database provider (MongoDB, PostgreSQL, SqlServer)" -ForegroundColor White  
    Write-Host "  --UseSwagger         Include Swagger documentation (true/false)" -ForegroundColor White
    Write-Host "  --IncludeTests       Include test projects (true/false)" -ForegroundColor White
    Write-Host "  --DatabaseName       Default database name" -ForegroundColor White
    Write-Host ""
    Write-Host "📖 Example:" -ForegroundColor Cyan
    Write-Host "  dotnet new dotnet-backend -n MyAPI --DatabaseProvider MongoDB --UseSwagger true" -ForegroundColor White
}
catch {
    Write-Host "❌ Failed to install template: $_" -ForegroundColor Red
    exit 1
}
