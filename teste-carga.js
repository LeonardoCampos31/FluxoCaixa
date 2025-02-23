import http from 'k6/http';
import { check } from 'k6';

export const options = {
  stages: [
    { duration: '1m', target: 50 }, // 50 req/seg por 1 minuto
  ],
  thresholds: {
    http_req_failed: ['rate<0.05'], // Máximo 5% de erro
    http_req_duration: ['p(95)<500'], // 95% das requisições em menos de 500ms
  },
};

export default function () {
    let res = http.get('http://host.docker.internal:5004/api/consolidado/2025-02-23T00:00:00Z', {
        headers: {
            'accept': '*/*',
            'Content-Type': 'application/json',
        },
    });

    check(res, {
        'status é 200': (r) => r.status === 200,
    });
}