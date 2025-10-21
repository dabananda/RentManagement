import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';
import { Account } from '../_services/account';

export const authGuard: CanActivateFn = (route, state) => {
  const accountService = inject(Account);
  const router = inject(Router);

  if (!accountService.currentUser()) {
    router.navigate(['/login']);
    return false;
  }

  return true;
};
