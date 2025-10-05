import { ShopDetails } from './shop';
import { TenantDetails } from './tenant';

export interface AgreementWithTenant {
  id: number;
  rentAmount: number;
  securityFee: number;
  startDate: string;
  endDate?: string | null;
  isActive: boolean;
  tenantId: number;
  tenant: TenantDetails;
}

export interface AgreementWithShop {
  id: number;
  rentAmount: number;
  securityFee: number;
  startDate: string;
  endDate?: string | null;
  isActive: boolean;
  shop: ShopDetails;
}