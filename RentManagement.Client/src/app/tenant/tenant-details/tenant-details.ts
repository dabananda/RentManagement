import { Component, inject, OnInit, signal } from '@angular/core';
import { TenantService } from '../../_services/tenant.service';
import { Tenant } from '../../_models/tenant';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-tenant-details',
  imports: [],
  templateUrl: './tenant-details.html',
  styleUrl: './tenant-details.css',
})
export class TenantDetails implements OnInit {
  private tenantService = inject(TenantService);
  private route = inject(ActivatedRoute);
  tenant = signal<Tenant | null>(null);

  ngOnInit(): void {
    this.loadTenant();
  }

  loadTenant() {
    const id: number = Number(this.route.snapshot.paramMap.get('id'));
    this.tenantService.getTenant(id).subscribe({
      next: (tenant) => this.tenant.set(tenant),
    });
  }
}
