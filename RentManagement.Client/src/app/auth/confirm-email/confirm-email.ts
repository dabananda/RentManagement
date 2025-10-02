import { Component, inject, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Account } from '../../_services/account';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-confirm-email',
  imports: [NgIf],
  templateUrl: './confirm-email.html',
  styleUrl: './confirm-email.css',
})
export class ConfirmEmail implements OnInit {
  private route = inject(ActivatedRoute);
  private account = inject(Account);

  loading = true;
  message = '';
  error = '';

  ngOnInit(): void {
    const userId = this.route.snapshot.queryParamMap.get('userId') ?? '';
    const token = this.route.snapshot.queryParamMap.get('token') ?? '';

    if (!userId || !token) {
      this.loading = false;
      this.error = 'Missing confirmation parameters.';
      return;
    }

    this.account.confirmEmail(userId, token).subscribe({
      next: (res) =>
        (this.message = res?.message ?? 'Email confirmed successfully. You can now log in.'),
      error: (err) =>
        (this.error = err?.error?.Message ?? 'Email confirmation failed. Invalid link or user.'),
      complete: () => (this.loading = false),
    });
  }
}
