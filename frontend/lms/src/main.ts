import { bootstrapApplication } from '@angular/platform-browser';
import { provideHttpClient, withInterceptors } from '@angular/common/http'; // <-- Import these

import { App } from './app/app';
import { loadingInterceptor } from './app/interceptors/loading.interceptor';

bootstrapApplication(App, {
  providers: [
    // Provide HttpClient and register our interceptor
    provideHttpClient(
      withInterceptors([loadingInterceptor]) // <-- Register it here
    ),
    // ... other app-wide providers
  ]
});
