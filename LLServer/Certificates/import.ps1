param (
    [switch]$WhatIf = $false
)
$certsInMy = Get-ChildItem Cert:\LocalMachine\My
$certsInMy | Where-Object {$_.Issuer -match "Taito Arcade Machine CA"} | Remove-Item -WhatIf:$WhatIf

$certsInCurrent = Get-ChildItem Cert:\CurrentUser\My
$certsInCurrent | Where-Object {$_.Issuer -match "Taito Arcade Machine CA"} | Remove-Item -WhatIf:$WhatIf

$certsInRoot = Get-ChildItem Cert:\LocalMachine\Root
$certsInRoot | Where-Object {$_.Issuer -match "Taito Arcade Machine CA"} | Remove-Item -WhatIf:$WhatIf

Get-ChildItem -Path root.pfx | Import-PfxCertificate -CertStoreLocation Cert:\LocalMachine\My -WhatIf:$WhatIf
Get-ChildItem -Path nesys.pfx | Import-PfxCertificate -CertStoreLocation Cert:\LocalMachine\My -WhatIf:$WhatIf
Get-ChildItem -Path nesica1.pfx | Import-PfxCertificate -CertStoreLocation Cert:\LocalMachine\My -WhatIf:$WhatIf
Get-ChildItem -Path cert.pfx | Import-PfxCertificate -CertStoreLocation Cert:\LocalMachine\My -WhatIf:$WhatIf

Get-ChildItem -Path root.pfx | Import-PfxCertificate -CertStoreLocation Cert:\CurrentUser\My -WhatIf:$WhatIf
Get-ChildItem -Path nesys.pfx | Import-PfxCertificate -CertStoreLocation Cert:\CurrentUser\My -WhatIf:$WhatIf
Get-ChildItem -Path nesica1.pfx | Import-PfxCertificate -CertStoreLocation Cert:\CurrentUser\My -WhatIf:$WhatIf
Get-ChildItem -Path cert.pfx | Import-PfxCertificate -CertStoreLocation Cert:\CurrentUser\My -WhatIf:$WhatIf


Get-ChildItem -Path root.pfx | Import-PfxCertificate -CertStoreLocation Cert:\LocalMachine\Root -WhatIf:$WhatIf