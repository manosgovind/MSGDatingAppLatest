import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse, HTTP_INTERCEPTORS } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';

// interceptor class to handle the errors in our application
@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    intercept(
        req: HttpRequest<any>,
        next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req).pipe(
            catchError(error => {
                if (error.status === 401) { // for 401 errors
                    return throwError(error.statusText);
                }
                if (error instanceof HttpErrorResponse) {
                    // tslint:disable-next-line: max-line-length
                    // to get the error message we set in the header part of he API request.
                    // The name Application-Error should match with the name we defined in the  extension method to add application error.
                    const applicationError = error.headers.get('Application-Error');

                    if (applicationError) {
                        return throwError(applicationError);
                    }

                    // handle modal state error and other validation errors - error which we set in our modals like required, maxlength etc.
                    const serverError = error.error; // validation error like username already exists
                    let modalStateErrors = '';         // modal errors like required, string length etc

                    if (serverError.errors && typeof serverError.errors === 'object') {
                        for (const key in serverError.errors) {
                            if (serverError.errors[key]) {
                                modalStateErrors += serverError.errors[key] + '\n';
                            }
                        }
                    }
                    return throwError(modalStateErrors || serverError || 'Unknown Server Error');

                }
            })
        );
        // throw new Error('Method not implemented.');
    }
}

// msg - this is to be added in our providers in app.modules.ts
export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true
};
