import { Component, inject } from '@angular/core';
import {
  ReactiveFormsModule,
  FormBuilder,
  Validators,
  AbstractControl,
  ValidationErrors,
} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Account } from '../../_services/account';

function matchPasswords(ctrl: AbstractControl): ValidationErrors | null {
  const pw = ctrl.get('password')?.value;
  const cpw = ctrl.get('confirmPassword')?.value;
  return pw && cpw && pw !== cpw ? { passwordMismatch: true } : null;
}

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
})
export class Register {
  private fb = inject(FormBuilder);
  private account = inject(Account);

  loading = false;
  serverMessage = '';
  serverError = '';

  form = this.fb.group(
    {
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(4)]],
      confirmPassword: [''],
    },
    { validators: matchPasswords }
  );

  get f() {
    return this.form.controls;
  }

  submit() {
    if (this.form.invalid) return;
    this.loading = true;
    this.serverMessage = '';
    this.serverError = '';

    const { email, password } = this.form.getRawValue();
    this.account.register({ email: email!, password: password! }).subscribe({
      next: (res) => {
        this.serverMessage = res?.message ?? 'Registered. Please confirm your email.';
      },
      error: (err) => {
        this.serverError = err?.error?.Message ?? 'Registration failed.';
      },
      complete: () => (this.loading = false),
    });
  }
}
