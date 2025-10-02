import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'login',
    loadComponent: () => import('../app/auth/login/login').then((m) => m.Login),
  },
  {
    path: 'register',
    loadComponent: () => import('../app/auth/register/register').then((m) => m.Register),
  },
  {
    path: 'confirm-email',
    loadComponent: () =>
      import('../app/auth/confirm-email/confirm-email').then((m) => m.ConfirmEmail),
  },
];
