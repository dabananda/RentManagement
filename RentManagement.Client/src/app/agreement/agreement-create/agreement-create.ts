import { AgreementCreate as Agreement } from '../../_models/agreement';
import { Component, inject, OnInit, signal } from '@angular/core';
import { AgreementService } from '../../_services/agreement.service';
import { ShopService } from '../../_services/shop.service';
import { TenantService } from '../../_services/tenant.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ShopList } from '../../_models/shop';
import { Tenant } from '../../_models/tenant';

@Component({
  selector: 'app-agreement-create',
  imports: [ReactiveFormsModule],
  templateUrl: './agreement-create.html',
  styleUrl: './agreement-create.css',
})
export class AgreementCreate implements OnInit {
  private agreementService = inject(AgreementService);
  private shopService = inject(ShopService);
  private tenantService = inject(TenantService);
  private router = inject(Router);
  private fb = inject(FormBuilder);

  formAgreement!: FormGroup;
  loading = signal<boolean>(false);
  error = signal<string | null>(null);

  availableShops = signal<ShopList[]>([]);
  availableTenants = signal<Tenant[]>([]);

  ngOnInit(): void {
    this.initForm();
    this.loadAvailableShops();
    this.loadAvailableTenants();
  }

  initForm(): void {
    this.formAgreement = this.fb.group({
      rentAmount: ['', Validators.required],
      securityFee: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: [''],
      tenantId: ['', Validators.required],
      shopId: ['', Validators.required],
    });
  }

  loadAvailableShops(): void {
    const unoccupiedShops = this.shopService.shops().filter((shop) => !shop.isOccupied);
    this.availableShops.set(unoccupiedShops);
  }

  loadAvailableTenants(): void {
    const eligibleTenants = this.tenantService
      .tenants()
      .filter((tenant) => !tenant.agreements.some((agreement) => agreement.isActive));
    this.availableTenants.set(eligibleTenants);
  }

  onSubmit(): void {
    if (this.formAgreement.invalid) {
      this.formAgreement.markAllAsTouched();
      return;
    }

    this.loading.set(true);
    this.error.set(null);

    const formValue = this.formAgreement.value;
    const agreement: Agreement = {
      rentAmount: Number(formValue.rentAmount),
      securityFee: Number(formValue.securityFee),
      startDate: formValue.startDate,
      endDate: formValue.endDate || null,
      tenantId: Number(formValue.tenantId),
      shopId: Number(formValue.shopId),
    };

    this.agreementService.createAgreement(agreement).subscribe({
      next: () => {
        this.loading.set(false);
        this.router.navigate(['/agreements']);
      },
      error: (err) => {
        this.error.set(err.error?.Message || err.message || 'An error occurred');
        this.loading.set(false);
      },
    });
  }

  cancel(): void {
    this.router.navigate(['/agreements']);
  }
}
