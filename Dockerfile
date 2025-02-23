# Usar a versão mais recente do k6
FROM loadimpact/k6

# Copiar o script do teste de carga para o contêiner
COPY teste-carga.js /teste-carga.js

# Comando para rodar o k6 com o script
CMD ["run", "/teste-carga.js"]