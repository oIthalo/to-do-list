import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter, withHashLocation } from '@angular/router';
import { APP_ROUTES } from './app.routes';
import { HTTP_INTERCEPTORS, provideHttpClient, withInterceptors, withInterceptorsFromDi } from '@angular/common/http';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import { AuthInterceptor } from './auth.interceptor';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(APP_ROUTES, withHashLocation()),
    provideAnimationsAsync(),
    {
        provide: HTTP_INTERCEPTORS,
        useFactory: () => AuthInterceptor,
        multi: true
    },
    provideHttpClient(
      withInterceptors([AuthInterceptor])
    )
  ]
};
