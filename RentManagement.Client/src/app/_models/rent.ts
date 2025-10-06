import { AgreementWithTenant } from './agreement';
import { ShopDetails } from './shop';

export interface Rent {
  id: number;
  year: number;
  month: number;
  amount: number;
  paidOn: string;
  notes: string | null;
  agreement: AgreementWithTenant;
  shop: ShopDetails;
}

export interface RentCreate {
  shopId: number;
  year: number;
  month: number;
  amount: number;
  notes: string | null;
}
