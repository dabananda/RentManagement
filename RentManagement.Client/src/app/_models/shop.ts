import { AgreementWithTenant } from './agreement';

export interface ShopList {
  id: number;
  shopNumber: string;
  areaSqFt: number;
  floor: string;
  isOccupied: boolean;
  agreements: AgreementWithTenant[];
}
