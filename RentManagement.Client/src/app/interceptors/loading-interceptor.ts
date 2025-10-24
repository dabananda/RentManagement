import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { LoaderService } from '../_services/loader.service';
import { finalize } from 'rxjs';

export const loadingInterceptor: HttpInterceptorFn = (req, next) => {
  const loader = inject(LoaderService);
  loader.show();
  return next(req).pipe(finalize(() => loader.hide()));
};
