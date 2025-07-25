function ci-up {
    docker compose -f compose.ci.yml up -d --build

    Write-Host "Waiting for TeamCity to become ready..."

    while ($true) {
        try {
            $response = Invoke-WebRequest -Uri http://localhost:8111 -UseBasicParsing -TimeoutSec 5
            if ($response.StatusCode -eq 200) {
                Write-Host "TeamCity is ready at http://localhost:8111"
                break
            }
        } catch {
            Write-Host -NoNewline "."
            Start-Sleep -Seconds 3
        }
    }
}