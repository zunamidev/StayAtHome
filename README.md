# Stay at Home

# Die Idee

Die App soll einen Anreiz bieten in gewissen Ausnahmesituationen die Distanz zu anderen Menschen zu wahren.

Dabei entstand die Idee ein Belohnungssystem für das Zuhause bleiben zu entwickeln.

Durch die Gamification entsteht ein fast sportlicher Anreiz sich an die neuen Vorgaben zu halten.

# Bisherige Umsetzung

Grafisches User Interface für:

* Ansicht der Zeiten des Aufenthaltes
* Kartenansicht:
  * Zuhause
  * Aktueller Aufenthaltsort
* Anmeldebildschirm

Sowie Hintergrund Location-Tracking und Berechnung der "Zuhause-Zeit"

Der Nutzer wird einmalig dazu aufgefordert seinen Heimatstandort zu speichern. Dieser kann nicht geändert werden. 

Ein Vergleich von aktuellem und gespeichertem Standort findet statt. Es soll lediglich die Information an einen Server übermittelt werden, ob diese grob übereinstimmen. Standorte sollen nicht übermittelt werden.

# Zukunftsausblick

* Überlegungen zur Implementierung eines Belohnungssystems:
  * Punkte-Kriterien:
     * userLocation entspricht homeLocation => wenn Distanz < 20m (abhängig von Genauigkeit des Standort-Trackings).
     * Je mehr Zeit zuhause verbracht wird, desto mehr Punkte können verdient werden.
     * Berücksichtigung der Tageszeit / des Wochentages, um ein Punktefarmen zu verhindern.
     * (Ansatz für einen dynamischen Multiplikator)
   * Belohnungen:
     * Zielgruppenbasierte Belohnungen:
          * Auswahl aus verschiedenen Kategorien, um jede Alters- und Interessengruppe anzusprechen.
     * Nützliche Dinge des Alltags:
          * z.B. Rabatt-Coupons für Lebensmittelgeschäfte und Drogeriemärkte (Essentials)

# ToDos:
  * Server zur Überprüfung der “Heimzeit” der Nutzer und zentralen Erfassung der Punkte eines Nutzers
  * Gewinn von Partnern zur Umsetzung des Belohnungssystems
    * Vorteile für Partner: Erweiterung des Kundenstamms

# Team

* Hamza Huber (github: zunamidev)
* Pascal Leist (github: reppentenner)
* Marian Theisen (github: cice)


# Unser Devpost Beitrag
[Devpost](https://devpost.com/software/stay-at-home-hc38ep)
