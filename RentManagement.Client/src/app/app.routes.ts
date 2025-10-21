import { Routes } from '@angular/router';
import { Login } from './auth/login/login';
import { Register } from './auth/register/register';
import { ConfirmEmail } from './auth/confirm-email/confirm-email';
import { NotFound } from './not-found/not-found';
import { Home } from './home/home';
import { ShopList } from './shops/shop-list/shop-list';
import { Shop } from './shops/shop/shop';
import { ShopUpdateComponent } from './shops/shop-update/shop-update';
import { ShopCreate } from './shops/shop-create/shop-create';
import { TenantList } from './tenant/tenant-list/tenant-list';
import { TenantDetails } from './tenant/tenant-details/tenant-details';
import { TenantCreate } from './tenant/tenant-create/tenant-create';
import { TenantUpdate } from './tenant/tenant-update/tenant-update';
import { AgreementList } from './agreement/agreement-list/agreement-list';
import { AgreementDetails } from './agreement/agreement-details/agreement-details';
import { AgreementCreate } from './agreement/agreement-create/agreement-create';
import { AgreementUpdate } from './agreement/agreement-update/agreement-update';
import { AgreementEnd } from './agreement/agreement-end/agreement-end';
import { RentList } from './rent/rent-list/rent-list';
import { authGuard } from './guards/auth-guard';
import { guestGuard } from './guards/guest-guard';

export const routes: Routes = [
  { path: '', component: Home, title: 'Home Page' },
  { path: 'login', component: Login, canActivate: [guestGuard], title: 'Login Page' },
  { path: 'register', component: Register, canActivate: [guestGuard], title: 'Registration Page' },
  {
    path: 'confirm-email',
    component: ConfirmEmail,
    canActivate: [guestGuard],
    title: 'Confirm Email',
  },

  {
    path: 'shops',
    canActivate: [authGuard],
    children: [
      { path: '', component: ShopList, title: 'Shop List' },
      { path: 'create', component: ShopCreate, title: 'Create Shop' },
      { path: ':id', component: Shop, title: 'Shop Details' },
      { path: ':id/update', component: ShopUpdateComponent, title: 'Update Shop' },
    ],
  },

  {
    path: 'tenants',
    canActivate: [authGuard],
    children: [
      { path: '', component: TenantList, title: 'Tenant List' },
      { path: 'create', component: TenantCreate, title: 'Tenant Create' },
      { path: ':id', component: TenantDetails, title: 'Tenant Details' },
      { path: ':id/update', component: TenantUpdate, title: 'Tenant Update' },
    ],
  },

  {
    path: 'agreements',
    canActivate: [authGuard],
    children: [
      { path: '', component: AgreementList, title: 'Agreement List' },
      { path: 'create', component: AgreementCreate, title: 'Agreement Create' },
      { path: ':id', component: AgreementDetails, title: 'Agreement Details' },
      { path: ':id/update', component: AgreementUpdate, title: 'Agreement Update' },
      { path: ':id/end', component: AgreementEnd, title: 'Agreement End' },
    ],
  },

  { path: 'rents', component: RentList, canActivate: [authGuard], title: 'Rent List' },
  { path: '**', component: NotFound, title: 'Not Found' },
];
