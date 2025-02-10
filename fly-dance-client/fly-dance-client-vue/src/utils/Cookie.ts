import Cookies from 'js-cookie'

class CookieUitl {
  getCookie(key: string): string | undefined {
    return Cookies.get(key)
  }

  setCookie(key: string, value: string, saveDay: number, options?: Cookies.CookieAttributes): void {
    Cookies.set(key, value, { ...options, expires: saveDay, sameSite: 'None' })
  }

  removeCookie(key: string): void {
    Cookies.remove(key)
  }
}

export const getCookie = () => {
  return new CookieUitl()
}
