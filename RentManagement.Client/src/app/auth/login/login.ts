import { Component, inject } from '@angular/core';
import { Account } from '../../_services/account';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, NgIf, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private accountService = inject(Account);
  private fb = inject(FormBuilder);
  private router = inject(Router);

  loading = false;
  serverMessage = '';
  serverError = '';

  form = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required, Validators.minLength(4)]],
  });

  login() {
    if (this.form.invalid) return;

    this.loading = true;
    this.serverError = '';
    this.serverMessage = '';

    const loginData = {
      email: this.form.value.email!,
      password: this.form.value.password!,
    };

    this.accountService.login(loginData).subscribe({
      next: () => {
        this.serverMessage = 'Login successful!';
        this.loading = false;
        this.router.navigate(['/dashboard']);
      },
      error: (error) => {
        console.error('Login error:', error);
        this.serverError =
          error.error?.Message || error.error?.message || 'Login failed. Please try again.';
        this.loading = false;
      },
    });
  }

  get f() {
    return this.form.controls;
  }
}
