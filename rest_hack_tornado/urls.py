from handlers.foo import FooHandler

url_patterns = [
    (r"/", MainHandler),
    (r"/login", LoginHandler),
    (r"/foo", FooHandler),
]
