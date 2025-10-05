import { computed, Injectable, signal } from '@angular/core';
import { AuthState } from '../_models/auth-state';

const KEY = 'auth';

@Injectable({
  providedIn: 'root',
})
export class AuthStoreService {
  private _state = signal<AuthState | null>(this.read());

  userKey = computed(() => this._state()?.email ?? null);
  
  get state() {
    return this._state();
  }

  get token() {
    return this._state()?.token ?? null;
  }

  set(state: AuthState) {
    localStorage.setItem(KEY, JSON.stringify(state));
    this._state.set(state);
  }

  clear() {
    localStorage.removeItem(KEY);
    this._state.set(null);
  }

  private read(): AuthState | null {
    const raw = localStorage.getItem(KEY);
    if (raw) return JSON.parse(raw) as AuthState;

    const legacyUser = localStorage.getItem('user');
    const legacyToken = localStorage.getItem('token');
    if (legacyUser) {
      try {
        const u = JSON.parse(legacyUser);
        const token = u.token ?? legacyToken ?? null;
        if (token) {
          const migrated: AuthState = { email: u.email, token, roles: u.roles ?? [] };
          localStorage.setItem(KEY, JSON.stringify(migrated));
          localStorage.removeItem('user');
          localStorage.removeItem('token');
          return migrated;
        }
      } catch {}
    }
    return null;
  }
}
