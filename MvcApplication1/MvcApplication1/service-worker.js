//chrome 41 では cache.add, cache.addAll がまだ存在していないのでライブラリを使う
importScripts('/Scripts/serviceworker-cache-polyfill.js');

//キャッシュ名。ファイルのバージョンが変更になった場合ここを変えることで制御する。
var CACHE_NAME = 'json-cache-v00009';

console.log(CACHE_NAME);

//キャッシュ対象のURL
var cachedUrls = [
        '/Demo/Index',
];

//インストールイベント発生時に呼ばれる。
self.addEventListener('install', function (event) {
    console.log('インストール成功！');
    event.waitUntil(
      caches.open(CACHE_NAME)
        .then(function (cache) {
            console.log('キャッシュ対象に加えます！');
            console.log(cache);
            try {
                //この段階でサーバにリクエストを発行して事前にキャッシュを生成しています。
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
    console.log("アクティベート！");
    //キャッシュ対象変更時、古いヴァージョンのキャッシュを削除する。
    var cacheWhitelist = [CACHE_NAME];
    event.waitUntil(
      caches.keys().then(function (cacheNames) {
          return Promise.all(
            cacheNames.map(function (cacheName) {
                if (cacheWhitelist.indexOf(cacheName) === -1) {
                    console.log("キャッシュ削除");
                    console.log(cacheName);
                    //キャッシュ削除。
                    return caches.delete(cacheName);
                }
            })
          );
      })
    );
});

//フェッチイベント発生時に呼ばれる。
self.addEventListener('fetch', function (event) {

    console.log('リクエストをフェッチしました。');
    //console.log( event.request.url ) ;
    event.respondWith(
      caches.match(event.request)
        .then(function (response) {
            console.log('キャッシュの検証をします。。');
            if (response) {
                console.log('キャッシュがあったのでレスポンスはキャッシュから返す！');
                console.log(response);
                return response;
            }

            var fetchRequest = event.request.clone();

            return fetch(fetchRequest, { mode: 'no-cors' })
              .then(function (response) {
                  console.log("フェッチリクエストで、レスポンスの精査をします");
                  // レスポンスが正しいかをチェック
                  if (!response || response.status !== 200 || response.type !== 'basic') {
                      return response;
                  }

                  // 重要：レスポンスを clone する。レスポンスは Stream で
                  // ブラウザ用とキャッシュ用の2回必要。なので clone して
                  // 2つの Stream があるようにする
                  var responseToCache = response.clone();

                  //あらゆる通信を根こそぎキャッシュしてみる。
                  caches.open(CACHE_NAME)
                    .then(function (cache) {
                        console.log('event.request をキャッシュに入れます。 ');
                        console.log(event.request);
                        //キャッシュします。次回からはリクエストされても通信せずにキャッシュから呼ばれます。
                        cache.put(event.request, responseToCache);
                    });

                  return response;
              });

        })
    );
});
