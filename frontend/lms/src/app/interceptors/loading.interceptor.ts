// src/app/core/interceptors/loading.interceptor.ts
import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { finalize } from 'rxjs/operators'; // Import finalize
import { LoadingService } from '../loadingComponent/loadingService';

// Export a const that is a function matching the HttpInterceptorFn type
export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loadingService = inject(LoadingService);


  loadingService.loadingOn();

  return next(req).pipe(
    finalize(() => {
      loadingService.loadingOf();
    })
  );
};