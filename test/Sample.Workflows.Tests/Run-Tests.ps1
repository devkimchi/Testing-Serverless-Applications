Param(
    [string] [Parameter(Mandatory=$true)] $FileDeleteSuccessUrl,
    [string] [Parameter(Mandatory=$true)] $FileDeleteFailUrl,
    [string] [Parameter(Mandatory=$true)] $DoNothingUrl
)

$exitCode = 0

Write-Host "Testing File Deletion Success..." -ForegroundColor Green

$result = Invoke-WebRequest -Method Post -Uri $FileDeleteSuccessUrl
if ($result.StatusCode -ne 200)
{
  Write-Host "File Delete Success test failed"
  $exitCode++
}

Write-Host "Testing File Deletion Fail..." -ForegroundColor Green

try
{
  $result = Invoke-WebRequest -Method Post -Uri $FileDeleteFailUrl
  if ($result.StatusCode -lt 400)
  {
    Write-Host "File Delete Fail test failed"
    $exitCode++
  }
}
catch
{
  $result = ([System.Net.WebException]$_.Exception).Response
  if ([int]$result.StatusCode -lt 400)
  {
    Write-Host "File Delete Fail test failed"
    $exitCode++
  }

}

Write-Host "Testing Do Nothing..." -ForegroundColor Green

$result = Invoke-WebRequest -Method Post -Uri $FileDeleteSuccessUrl
if ($result.StatusCode -ne 200)
{
  Write-Host "Do Nothing test failed"
  $exitCode++
}

if ($exitCode -gt 0)
{
  $host.SetShouldExit($exitCode)
}

Write-Host "All tests passed" -ForegroundColor Green

Remove-Variable result
Remove-Variable exitCode
