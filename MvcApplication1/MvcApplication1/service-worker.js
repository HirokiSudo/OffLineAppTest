//chrome では cache.add, cache.addAll がまだ存在していないのでライブラリを使う
importScripts('/Scripts/serviceworker-cache-polyfill.js');

//キャッシュ名。
var CACHE_NAME = 'OffLineAppTest-cache';

console.log(CACHE_NAME);

//静的キャッシュ対象のURL
var cachedUrls = [
        '/favicon.ico',
];

//インストールイベント発生時に呼ばれる。
self.addEventListener('install', function (event) {
    console.log('インストール完了');
    event.waitUntil(
      caches.open(CACHE_NAME)
        .then(function (cache) {
            try {
                //静的キャッシュを生成
                cache.addAll(cachedUrls);
            } catch (e) {
                console.log(e);
            }
            return cache;
        })
    );
});

//アクティベートイベント発生時に呼ばれる。
self.addEventListener('activate', function (event) {
    console.log('アクティベート完了');
});

//フェッチイベント発生時に呼ばれる。
self.addEventListener('fetch', function (event) {

    event.respondWith(
      caches.match(event.request)
        .then(function (response) {
            if (response) {
                console.log('レスポンスをキャッシュから返却 Url:' + event.request.url);
                return response;
            }

            var fetchRequest = event.request.clone();

            return fetch(fetchRequest, { mode: 'no-cors' })
              .then(function (response) {
                  // レスポンスチェック
                  if (!response || response.status !== 200 || response.type !== 'basic') {
                      console.log("レスポンスチェックエラー Url:" + event.request.url);
                      return response;
                  }

                  var responseToCache = response.clone();

                  // レスポンスをキャッシュに格納
                  caches.open(CACHE_NAME)
                    .then(function (cache) {
                        console.log('レスポンスをキャッシュに格納 Url:' + event.request.url);
                        cache.put(event.request, responseToCache);
                    });

                  return response;
              });

        })
    );
});
