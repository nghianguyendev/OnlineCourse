export const environment = {
  production: true
};

let providers: any[] = [
  { provide: 'apiUrl', useValue: 'https://localhost:44325' }
];

export const ENV_PROVIDERS = providers;
