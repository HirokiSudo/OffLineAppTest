// Cache API���ꕔ�������Ȃ̂Ń|���t�B�������[�h
importScripts('/serviceworker-cache-polyfill.js');

// �L���b�V���̃L�[�ƂȂ镶����
var CACHE_KEY = 'service-worker-playground-v1';

self.addEventListener('install', function (event) {

  console.log('ServiceWorker.oninstall: ', event);

  event.waitUntil(
    caches.open(CACHE_KEY).then(function (cache) {

      // cache�����������N�G�X�g�̃L�[��ǉ�
      return cache.addAll([
        '/',
        '/Demo/Index',
        '/data/column_domains.json',
        '/data/olympic_medalists.json',
        '/lib/angular.min.js',
        '/lib/bootstrap.min.css',
        '/lib/lovefield.min.js',
        '/lib/ngStorage.min.js',
        '/resources/chalk.png',
        '/resources/demo.css',
        '/resources/demo.js',
        '/service-worker.js',
      ]);
    })
  );
});

self.addEventListener('fetch', function (event) {

  console.log('ServiceWorker.onfetch: ', event);

  event.respondWith(
    fetch(event.request).catch(function () {
        return caches.match(event.request);
    })
  );
});

self.addEventListener('activate', function (event) {
    console.log('ServiceWorker.onactivate: ', event);
});