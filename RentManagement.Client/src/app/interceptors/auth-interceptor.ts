import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { TokenService } from '../_services/token.service';

const BYPASS = ['/auth/login', 'auth/register'];

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const tokenService = inject(TokenService);

  // skip if matched a bypassed path
  if (BYPASS.some((p) => req.url.includes(p))) {
    return next(req);
  }

  const token = tokenService.token;
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
