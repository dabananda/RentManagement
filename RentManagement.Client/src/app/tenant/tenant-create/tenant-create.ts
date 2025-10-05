import { Component, inject, OnInit, signal } from '@angular/core';
import { TenantService } from '../../_services/tenant.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-tenant-create',
  imports: [ReactiveFormsModule],
  templateUrl: './tenant-create.html',
  styleUrl: './tenant-create.css',
})
export class TenantCreate implements OnInit {
  private tenantService = inject(TenantService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  tenantForm!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  ngOnInit(): void {
    this.initForm();
  }

  initForm(): void {
    this.tenantForm = this.fb.group({
      name: ['', [Validators.required, Validators.min(2)]],
      phone: ['', [Validators.required, Validators.min(11)]],
      email: ['', [Validators.required]],
    });
  }

  onSubmit(): void {
    if (this.tenantForm.invalid) {
      this.tenantForm.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    this.tenantService.addTenant(this.tenantForm.value).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/tenants']);
      },
      error: (err) => {
        this.error.set(err);
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/tenants']);
  }
}
