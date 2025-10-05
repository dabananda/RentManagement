import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { tap } from 'rxjs';
import { LoginResponse } from '../_models/login-response';
import { ApiMessage } from '../_models/api-message';
import { TokenService } from './token.service';
import { User } from '../_models/user';

@Injectable({
  providedIn: 'root',
})
export class Account {
  private http = inject(HttpClient);
  private tokenService = inject(TokenService);
  private baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);

  login(model: { email: string; password: string }) {
    return this.http.post<LoginResponse>(`${this.baseUrl}/auth/login`, model).pipe(
      tap((res) => {
        if (res?.token) {
          const user: User = { email: model.email, token: res.token };
          localStorage.setItem('user', JSON.stringify(user));
          this.tokenService.set(user.token);
          this.currentUser.set(user);
        } else {
          console.log('No token in response');
        }
      })
    );
  }

  register(model: { email: string; password: string }) {
    return this.http.post<ApiMessage>(`${this.baseUrl}/auth/register`, model);
  }

  confirmEmail(userId: string, token: string) {
    const params = new HttpParams().set('userId', userId).set('token', token);
    return this.http.post<ApiMessage>(`${this.baseUrl}/auth/ConfirmEmail`, null, { params });
  }

  logout() {
    this.currentUser.set(null);
    this.tokenService.set(null);
  }
}
