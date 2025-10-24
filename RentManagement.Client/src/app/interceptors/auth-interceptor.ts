import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthStoreService } from '../_services/auth-store.service';

const BYPASS = ['/auth/login', 'auth/register', '/auth/confirm-email'];

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authStoreService = inject(AuthStoreService);

  // skip if matched a bypassed path
  if (BYPASS.some((p) => req.url.includes(p))) {
    return next(req);
  }

  const token = authStoreService.token;
  if (!token) {
    return next(req);
  }

  // attach header
  const authReq = req.clone({
    setHeaders: {
      Authorization: `Bearer ${token}`,
    },
  });

  return next(authReq);
};
