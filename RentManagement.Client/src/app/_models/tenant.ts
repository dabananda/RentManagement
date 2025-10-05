import { AgreementWithShop } from './agreement';

export interface TenantDetails {
  id: number;
  name: string;
  phone: string;
  email: string;
}

export interface Tenant {
  id: number;
  name: string;
  phone: string;
  email: string;
  agreements: AgreementWithShop[];
}
