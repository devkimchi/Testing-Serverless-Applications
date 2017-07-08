Param(
    [string] [Parameter(Mandatory=$true)] $OkUri,
    [string] [Parameter(Mandatory=$true)] $NotFoundUri,
    [string] [Parameter(Mandatory=$true)] $ErrorUri
)

$exitCode = 0

$result = Invoke-WebRequest -Method Post -Uri $OkUri
if ($result.StatusCode -ne 200)
{
  Write-Host "OK test failed"
  $exitCode++
}

try
{
  $result = Invoke-WebRequest -Method Post -Uri $NotFoundUri
  if ($result.StatusCode -ne 404)
  {
    Write-Host "Not Found test failed"
    $exitCode++
  }
}
catch
{
  $result = ([System.Net.WebException]$_.Exception).Response
  if ([int]$result.StatusCode -ne 404)
  {
    Write-Host "Not Found test failed"
    $exitCode++
  }

}

try
{
  $result = Invoke-WebRequest -Method Post -Uri $ErrorUri
  if ($result.StatusCode -lt 400)
  {
    Write-Host "Error test failed"
    $exitCode++
  }
}
catch
{
  $result = ([System.Net.WebException]$_.Exception).Response
  if ([int]$result.StatusCode -lt 400)
  {
    Write-Host "Error test failed"
    $exitCode++
  }
}

if ($exitCode -gt 0)
{
  $host.SetShouldExit($exitCode)
}

Write-Host "All tests passed"

Remove-Variable result
Remove-Variable exitCode
