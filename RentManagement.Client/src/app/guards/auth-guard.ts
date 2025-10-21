import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Account } from '../_services/account';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(Account);
  const isAuthenticated = accountService.isLoggedIn();
  const router = inject(Router);

  if (!isAuthenticated) {
    router.navigate(['/login']);
    return false;
  }

  return true;
};
