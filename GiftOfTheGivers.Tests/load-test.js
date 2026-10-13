import http from 'k6/http';
import { check, sleep } from 'k6';

export const options = {
  vus: 5,           // 5 concurrent users as required
  duration: '30s',  // 30-second test duration
};

export default function () {
  // Target your locally running ASP.NET Core application
  const url = 'http://localhost:5110';

  const response = http.get(url);

  // Validate HTTP response status and response time
  check(response, {
    'status is 200': (r) => r.status === 200,
    'response time is acceptable (< 500ms)': (r) => r.timings.duration < 500,
  });

  sleep(1);
}