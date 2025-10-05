import { AgreementWithTenant } from './agreement';

export interface ShopList {
  id: number;
  shopNumber: string;
  areaSqFt: number;
  floor: string;
  isOccupied: boolean;
  agreements: AgreementWithTenant[];
}

export interface ShopUpdate {
  shopNumber: string;
  areaSqFt: number;
  floor: string;
}

export interface ShopDetails {
  id: number;
  shopNumber: string;
  areaSqFt: number;
  floor: string;
  isOccupied: boolean;
}
