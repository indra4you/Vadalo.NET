Write-Host "Checking if Docker is running or not..."

( docker version 2>&1 ) | Out-Null

if ( 0 -ne $LASTEXITCODE ) {
    Write-Host ""
    Write-Host -ForegroundColor DarkRed "    > Docker is NOT running!"

    Write-Host "    > Do you want to start Docker?"
    Write-Host -NoNewline -ForegroundColor Cyan "    > [Y] Yes, [N] No & Exit: "
    $Confirmation = $Host.UI.RawUI.ReadKey("NoEcho,IncludeKeyDown").Character

    Write-Host ""

    if ( "Y" -eq $Confirmation -or "y" -eq $Confirmation ) {
        Write-Host -NoNewline "    > Starting Docker..."

        $DockerExe = 'C:\Program Files\Docker\Docker\Docker Desktop.exe'
        $DockerProcess = [Diagnostics.Process]::Start($DockerExe)
        $DockerProcess.WaitForExit()
    
        Start-Sleep -Seconds 45.0
    
        Write-Host ""
        Write-Host -ForegroundColor Green "    > Docker started successfully"
    } else {
        if ( "N" -ne $Confirmation -and "n" -ne $Confirmation ) {
            Write-Host -ForegroundColor Red "    > Wrong choice!"
        }

        Exit
    }
} else {
    Write-Host -ForegroundColor Green "Docker is running"
}

docker compose up -d