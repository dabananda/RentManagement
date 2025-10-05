import { HttpClient } from '@angular/common/http';
import { effect, inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { Account } from './account';
import { Tenant, TenantDetails } from '../_models/tenant';
import { of, tap } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class TenantService {
  private http = inject(HttpClient);
  private baseUrl = environment.apiUrl;
  private account = inject(Account);

  tenants = signal<Tenant[]>([]);

  constructor() {
    effect(() => {
      const user = this.account.currentUser();
      this.tenants.set([]);
      if (user) this.getTenants();
    });
  }

  getTenants() {
    return this.http.get<Tenant[]>(`${this.baseUrl}/tenant`).subscribe({
      next: (tenants) => this.tenants.set(tenants),
      error: (err) => console.log(err),
    });
  }

  getTenant(id: number) {
    const tenant = this.tenants().find((t) => t.id == id);
    if (tenant !== undefined) return of(tenant);
    return this.http.get<Tenant>(`${this.baseUrl}/tenant/${id}`);
  }

  addTenant(tenant: TenantDetails) {
    return this.http
      .post<TenantDetails>(`${this.baseUrl}/tenant`, tenant)
      .pipe(tap(() => this.getTenants()));
  }

  getTenantForUpdate(id: number) {
    return this.http.get<TenantDetails>(`${this.baseUrl}/tenant/${id}`);
  }

  updateTenant(id: number, tenant: TenantDetails) {
    return this.http
      .put<TenantDetails>(`${this.baseUrl}/tenant/${id}`, tenant)
      .pipe(tap(() => this.getTenants()));
  }

  deleteTenant(id: number) {
    return this.http.delete(`${this.baseUrl}/tenant/${id}`).pipe(
      tap(() => {
        this.getTenants();
      })
    );
  }
}
