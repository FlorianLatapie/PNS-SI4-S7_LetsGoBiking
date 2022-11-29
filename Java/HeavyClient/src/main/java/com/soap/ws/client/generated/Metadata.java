
package com.soap.ws.client.generated;

import javax.xml.bind.JAXBElement;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElementRef;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour Metadata complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="Metadata"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="attribution" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="engine" type="{http://schemas.datacontract.org/2004/07/RoutingServer}Engine" minOccurs="0"/&gt;
 *         &lt;element name="query" type="{http://schemas.datacontract.org/2004/07/RoutingServer}Query" minOccurs="0"/&gt;
 *         &lt;element name="service" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="timestamp" type="{http://www.w3.org/2001/XMLSchema}long" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Metadata", propOrder = {
    "attribution",
    "engine",
    "query",
    "service",
    "timestamp"
})
public class Metadata {

    @XmlElementRef(name = "attribution", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<String> attribution;
    @XmlElementRef(name = "engine", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<Engine> engine;
    @XmlElementRef(name = "query", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<Query> query;
    @XmlElementRef(name = "service", namespace = "http://schemas.datacontract.org/2004/07/RoutingServer", type = JAXBElement.class, required = false)
    protected JAXBElement<String> service;
    protected Long timestamp;

    /**
     * Obtient la valeur de la propriété attribution.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getAttribution() {
        return attribution;
    }

    /**
     * Définit la valeur de la propriété attribution.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setAttribution(JAXBElement<String> value) {
        this.attribution = value;
    }

    /**
     * Obtient la valeur de la propriété engine.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link Engine }{@code >}
     *     
     */
    public JAXBElement<Engine> getEngine() {
        return engine;
    }

    /**
     * Définit la valeur de la propriété engine.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link Engine }{@code >}
     *     
     */
    public void setEngine(JAXBElement<Engine> value) {
        this.engine = value;
    }

    /**
     * Obtient la valeur de la propriété query.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link Query }{@code >}
     *     
     */
    public JAXBElement<Query> getQuery() {
        return query;
    }

    /**
     * Définit la valeur de la propriété query.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link Query }{@code >}
     *     
     */
    public void setQuery(JAXBElement<Query> value) {
        this.query = value;
    }

    /**
     * Obtient la valeur de la propriété service.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getService() {
        return service;
    }

    /**
     * Définit la valeur de la propriété service.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setService(JAXBElement<String> value) {
        this.service = value;
    }

    /**
     * Obtient la valeur de la propriété timestamp.
     * 
     * @return
     *     possible object is
     *     {@link Long }
     *     
     */
    public Long getTimestamp() {
        return timestamp;
    }

    /**
     * Définit la valeur de la propriété timestamp.
     * 
     * @param value
     *     allowed object is
     *     {@link Long }
     *     
     */
    public void setTimestamp(Long value) {
        this.timestamp = value;
    }

}
