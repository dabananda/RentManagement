import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { User } from '../_models/user';
import { tap } from 'rxjs';
import { LoginResponse } from '../_models/login-response';
import { ApiMessage } from '../_models/api-message';

@Injectable({
  providedIn: 'root',
})
export class Account {
  private http = inject(HttpClient);
  currentUser = signal<User | null>(null);

  login(model: { email: string; password: string }) {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/Auth/login`, model).pipe(
      tap((res) => {
        if (res?.token) {
          const user: User = { email: model.email, token: res.token };
          localStorage.setItem('user', JSON.stringify(user));
          this.currentUser.set(user);
        } else {
          console.log('No token in response');
        }
      })
    );
  }

  register(model: { email: string; password: string }) {
    return this.http.post<ApiMessage>(`${environment.apiUrl}/Auth/register`, model);
  }

  confirmEmail(userId: string, token: string) {
    const params = new HttpParams().set('userId', userId).set('token', token);
    return this.http.post<ApiMessage>(`${environment.apiUrl}/Auth/ConfirmEmail`, null, { params });
  }

  logout() {
    localStorage.removeItem('user');
    this.currentUser.set(null);
  }
}
