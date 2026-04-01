$baseUrl = "http://localhost:3000/api"
Write-Host "`n--- BATERIA DE TESTES: API GESTÃO ALUNOS ---" -ForegroundColor Cyan

function Run-Test {
    param($Name, $Method, $Path, $Body = $null, $Headers = @{}, $Expected)
    try {
        $p = @{ Uri="$baseUrl$Path"; Method=$Method; Headers=$Headers; ErrorAction="Stop" }
        if ($Body) { $p.Body = $Body; $p.ContentType = "application/json" }
        $r = Invoke-WebRequest @p
        $s = [int]$r.BaseResponse.StatusCode
    } catch { $s = [int]$_.Exception.Response.StatusCode }

    if ($s -eq $Expected) { Write-Host "[PASS] $Name" -ForegroundColor Green }
    else { Write-Host "[FAIL] $Name (Esperado: $Expected, Obtido: $s)" -ForegroundColor Red }
}

Run-Test "Listar Alunos" "Get" "/alunos" $null @{} 200
Run-Test "Obter Aluno 1" "Get" "/alunos/1" $null @{} 200
Run-Test "Criar Sucesso" "Post" "/alunos" '{"nome":"A","idade":15,"curso":"Informatica"}' @{} 201
Run-Test "Erro Idade < 10" "Post" "/alunos" '{"nome":"B","idade":5,"curso":"Informatica"}' @{} 400
Run-Test "Erro Curso" "Post" "/alunos" '{"nome":"C","idade":15,"curso":"Errado"}' @{} 404
Run-Test "Atualizar PUT" "Put" "/alunos/1" '{"nome":"D"}' @{} 200
Run-Test "Delete Forbidden" "Delete" "/alunos/1" $null @{} 403
Run-Test "Delete Admin OK" "Delete" "/alunos/1" $null @{"Role"="Admin"} 204

Write-Host "--- TESTES FINALIZADOS ---" -ForegroundColor Cyan