import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { ConfirmEmail } from './auth/confirm-email/confirm-email';
import { NotFound } from './not-found/not-found';
import { Home } from './home/home';
import { ShopList } from './shops/shop-list/shop-list';
import { Shop } from './shops/shop/shop';

export const routes: Routes = [
  { path: '', component: Home, title: 'Home Page' },
  { path: 'login', component: Login, title: 'Login Page' },
  { path: 'register', component: Register, title: 'Registration Page' },
  { path: 'confirm-email', component: ConfirmEmail, title: 'Confirm Email' },
  { path: 'shops', component: ShopList, title: 'Shop List' },
  { path: 'shop/:id', component: Shop, title: 'Shop Details' },
  { path: '**', component: NotFound, title: 'Not Found' },
];
