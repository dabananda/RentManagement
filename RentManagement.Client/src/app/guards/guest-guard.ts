import { CanActivateFn, Router } from '@angular/router';
import { Account } from '../_services/account';
import { inject } from '@angular/core';

export const guestGuard: CanActivateFn = (route, state) => {
  const accountService = inject(Account);
  const router = inject(Router);

  if (accountService.currentUser()) {
    router.navigate(['/']);
    return false;
  }

  return true;
};
