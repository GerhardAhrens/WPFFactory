# WPF Factory

![NET](https://img.shields.io/badge/NET-10-green.svg)
![License](https://img.shields.io/badge/License-MIT-blue.svg)
![VS2026](https://img.shields.io/badge/Visual%20Studio-2026-white.svg)
![Version](https://img.shields.io/badge/Version-1.0.2026.1-yellow.svg)

# Projekt

Das Projekt zeigt den Einsatz des Factory-Design-Patterns in einer WPF-Anwendung. Es bietet eine einfache Möglichkeit, verschiedene Komponenten zu erstellen und zu verwalten, ohne dass der Benutzer sich um die Details der Implementierung kümmern muss. Zusätzlich wird auch der EventAggregator eingesetzt um eine Anwendungsübergreifende Dialog-Navigation zu ermöglichen.

<img src="MainWindow.png" style="width:650px;"/>


# Features
Eine **Factory-Klasse** bietet Methoden zur Erstellung von Instanzen von Klassen, entweder als *Transient* (neue Instanz bei jedem Aufruf) oder als *Singleton* (gleiche Instanz bei jedem Aufruf). Dadurch wird die Verwaltung von Abhängigkeiten und die Erstellung von Objekten in der Anwendung erleichtert.

# Möglichkeiten

Über die Factory können verschiedene Typen registriert und Instanzen zurückgegeben werden, ohne dass der Benutzer sich um die Details der Implementierung kümmern muss. Dies ermöglicht eine flexible und modulare Gestaltung der Anwendung.
</br>
Auch können optionale Parameter bei der Erstellung von Instanzen verwendet werden, um die Flexibilität weiter zu erhöhen.
</br>
Ein weitere wichtiger Punkt ist, dass verschiedene `Enum`-Werte als Schlüssel für die Registrierung und Rückgabe von Instanzen verwendet werden können, was die Übersicht und Wartbarkeit des Codes verbessert.

## Registrieren

- Registrierung von Typen als Singleton oder Transient
- Verwendung von optionalen Parametern bei der Erstellung von Instanzen

```csharp
private void RegisterFactory()
{
    Factory.RegisterSingleton<DialogView>(DialogView.Home, () => new HelloUC());
    Factory.RegisterTransient<DialogView>(DialogView.DialogOverView, (param) => new DialogOverviewUC((ChangeViewEventArgs)param!));
    Factory.RegisterTransient<DialogView>(DialogView.DialogEdit, (param) => new DialogEditUC((ChangeViewEventArgs)param!));
}
```

## Zurückgeben von Instanzen

```csharp
private void GetInstance()
{
    var home = Factory.GetInstance<DialogView>(DialogView.Home);
    var overview = Factory.GetInstance<DialogView>(DialogView.DialogOverView, new ChangeViewEventArgs());
    var edit = Factory.GetInstance<DialogView>(DialogView.DialogEdit, new ChangeViewEventArgs());
}
```

![Version](https://img.shields.io/badge/Version-1.0.2026.1-yellow.svg)
- Migration auf NET 10
- Weiterentwicklung mit neuen Features
- Neues Design und Demoprogramm
