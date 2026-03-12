import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

const oAuthConfig = {
  issuer: 'https://localhost:44355/',
  redirectUri: baseUrl,
  clientId: 'WorkiomProjectManagement_App',
  responseType: 'code',
  scope: 'offline_access WorkiomProjectManagement',
  requireHttps: true,
};

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'WorkiomProjectManagement',
  },
  oAuthConfig,
  apis: {
    default: {
      url: 'https://localhost:44355',
      rootNamespace: 'WorkiomProjectManagement',
    },
    AbpAccountPublic: {
      url: oAuthConfig.issuer,
      rootNamespace: 'AbpAccountPublic',
    },
  },
  remoteEnv: {
    url: '/getEnvConfig',
    mergeStrategy: 'deepmerge'
  }
} as Environment;
