import { Injectable } from "@angular/core";
import { Http, Headers, RequestOptions, Response } from "@angular/http";
// import { Store } from '@ngrx/store';
import { Observable, pipe } from "rxjs";
import { map, retry, catchError } from "rxjs/operators";
import { AuthenticationService } from 'src/shared/services/authentication.service';

/** Services */
// import { AppState } from './../app.state';
// import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class SecureApi {
  private token: string;
  public api: string = "";

  constructor(private http: Http, private oauthService: AuthenticationService) {}

  get(resource: string) {
    let url = this.api + resource;

    return this.http
      .get(url, this.createRequestOptions())
      .pipe(
          map(this.mapData),
       catchError (error => this.handleError(error))
       );
  }

  put(resource: string, body: any) {
    let url = this.api + resource;

    return this.http
      .put(url, body, this.createRequestOptions())
      .pipe(
      map(this.mapData),
      catchError(error => this.handleError(error)));
  }

  post(resource: string, body: any, isUpload: boolean = false) {
    let url = this.api + resource;
    let headerOptions = this.createRequestOptions(isUpload);

    return this.http
      .post(url, body, headerOptions).pipe(
      map(this.mapData),
      catchError(error => this.handleError(error)));
  }

  delete(resource: string) {
    let url = this.api + resource;

    return this.http
      .delete(url, this.createRequestOptions()).pipe(
      map(this.mapData),
      catchError(error => this.handleError(error)));
  }

  private createRequestOptions(isUpload: boolean = false) {
    let contentType = isUpload ? "x-www-form-urlencoded" : "json";
    if (this.oauthService) {
      this.token = this.oauthService.getAccessToken();
    }
    let headerOptions = {
      Accept: `application/${contentType}`,
      "Access-Control-Allow-Credentials": "true",
      "Cache-Control": "no-cache",
      Pragma: "no-cache",
      Authorization: "Bearer " + this.token
    };

    let headers = new Headers(headerOptions);

    return new RequestOptions({ headers: headers });
  }

  private handleError(error: any) {
    if (error) {
      // un Authentication error
      console.error("rest error: ", error.status);
      if (error.status === 401) {
        // refresh the page then it will automatic do the login effect
        // if (!this.oauthService || !this.oauthService.hasValidAccessToken() || !this.oauthService.hasValidIdToken()) {
        //   window.location.reload();
        // }
      }
      return Observable.throw(error);
    }
    return Observable.throw(error.json().message || "Server error");
  }

  private mapData(res: Response): any {
    let body;
    try {
      // check if empty, before call json
      if (res.text()) {
        body = res.json();
      }
    } catch (error) {
      // Its not JSON, return a text
      body = res.text();
    }
    return body || {};
  }
}
