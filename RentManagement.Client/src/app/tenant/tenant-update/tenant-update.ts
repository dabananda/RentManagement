import { Component, inject, OnInit, signal } from '@angular/core';
import { TenantService } from '../../_services/tenant.service';
import { ActivatedRoute, Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { TenantDetails } from '../../_models/tenant';

@Component({
  selector: 'app-tenant-update',
  imports: [ReactiveFormsModule],
  templateUrl: './tenant-update.html',
  styleUrl: './tenant-update.css',
})
export class TenantUpdate implements OnInit {
  private tenantService = inject(TenantService);
  private router = inject(Router);
  private fb = inject(FormBuilder);
  private route = inject(ActivatedRoute);

  tenant = signal<TenantDetails | null>(null);
  tenantForm!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.initForm();
    this.loadTenant();
  }

  initForm(): void {
    this.tenantForm = this.fb.group({
      name: ['', [Validators.required, Validators.min(2)]],
      phone: ['', [Validators.required, Validators.min(11)]],
      email: ['', [Validators.required]],
    });
  }

  loadTenant(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    if (!id) {
      this.error.set('Invalid tenant ID');
      return;
    }

    this.loading.set(true);

    this.tenantService.getTenantForUpdate(id).subscribe({
      next: (tenant) => {
        this.tenant.set(tenant);
        this.tenantForm.patchValue({
          name: tenant.name,
          phone: tenant.phone,
          email: tenant.email,
        });
        this.loading.set(false);
      },
      error: (err) => {
        console.log(err);
        this.error.set('Failed to load');
        this.loading.set(false);
      },
    });
  }

  onSubmit(): void {
    if (this.tenantForm.invalid) {
      this.tenantForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const id = Number(this.route.snapshot.paramMap.get('id'));

    this.tenantService.updateTenant(id, this.tenantForm.value).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/tenants']);
      },
      error: (err) => {
        console.error('Error updating tenant:', err);
        this.error.set('Failed to upate tenant');
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/tenants']);
  }
}
