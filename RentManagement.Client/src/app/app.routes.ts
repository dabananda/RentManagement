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

export const routes: Routes = [
  { path: '', component: Home, title: 'Home Page' },
  { path: 'login', component: Login, title: 'Login Page' },
  { path: 'register', component: Register, title: 'Registration Page' },
  { path: 'confirm-email', component: ConfirmEmail, title: 'Confirm Email' },
  { path: 'shops', component: ShopList, title: 'Shop List' },
  { path: 'shop/create', component: ShopCreate, title: 'Create Shop' },
  { path: 'shop/:id', component: Shop, title: 'Shop Details' },
  { path: 'shop/:id/update', component: ShopUpdateComponent, title: 'Update Shop' },
  { path: 'tenants', component: TenantList, title: 'Tenant List' },
  { path: 'tenant/create', component: TenantCreate, title: 'Tenant Create' },
  { path: 'tenant/:id', component: TenantDetails, title: 'Tenant Details' },
  { path: 'tenant/:id/update', component: TenantUpdate, title: 'Tenant Details' },
  { path: 'agreements', component: AgreementList, title: 'Agreement List' },
  { path: 'agreement/create', component: AgreementCreate, title: 'Agreement Create' },
  { path: 'agreement/:id', component: AgreementDetails, title: 'Agreement Details' },
  { path: 'agreement/:id/update', component: AgreementUpdate, title: 'Agreement Details' },
  { path: 'agreement/:id/end', component: AgreementEnd, title: 'Agreement Details' },
  { path: '**', component: NotFound, title: 'Not Found' },
];
