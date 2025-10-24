import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';
import { LoginResponse } from '../_models/login-response';
import { ApiMessage } from '../_models/api-message';
import { User } from '../_models/user';
import { AuthStoreService } from './auth-store.service';
import { Router } from '@angular/router';
import { AuthState } from '../_models/auth-state';

@Injectable({
  providedIn: 'root',
})
export class Account {
  private http = inject(HttpClient);
  private authStoreService = inject(AuthStoreService);
  private router = inject(Router);
  private baseUrl = environment.apiUrl;

  currentUser = signal<User | null>(null);

  login(model: { email: string; password: string }) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`, model).pipe(
      tap((res) => {
        if (!res?.token) return;

        const state: AuthState = {
          email: res.email,
          token: res.token,
          roles: res.roles ?? [],
        };

        this.authStoreService.set(state);
        this.currentUser.set({ email: state.email, token: state.token });
      })
    );
  }

  register(model: { email: string; password: string }) {
    return this.http.post<ApiMessage>(`${this.baseUrl}/auth/register`, model);
  }

  confirmEmail(userId: string, token: string) {
    const params = new HttpParams().set('userId', userId).set('token', token);
    return this.http.post<ApiMessage>(`${this.baseUrl}/auth/confirm-email`, null, { params });
  }

  hydrateFromStore() {
    const s = this.authStoreService.state;
    if (s) this.currentUser.set({ email: s.email, token: s.token });
  }

  logout() {
    this.authStoreService.clear();
    this.currentUser.set(null);
    this.router.navigate(['/auth/login']);
  }
}
